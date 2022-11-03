// statisticsMonthController.js

(function() {
	"use strict";

	angular.module("app-statistics").controller("statisticsMonthController", statisticsMonthController);

	function statisticsMonthController($scope, $http, $filter, $routeParams, $location) {
		var vm = this;

		vm.owner = $routeParams.owner;
		vm.params = $location.search();
		vm.model = {};
		vm.months = [];
		vm.records = [];

		vm.uriTags = [];

		$scope.$watch("vm.selectedMonth", function() {
			vm.changeMonth(vm.selectedMonth);
		});

		vm.getMonths = function() {
			var today = new Date();
			var endDate = new Date();
			today.setDate(1);
			endDate.setMonth(endDate.getMonth() - 12);

			for (var d = today; d > endDate; d.setMonth(d.getMonth() - 1)) {
				vm.months.push($filter("date")(d, "yyyy") + "-" + ($filter("date")(d, "MM")));
			}
		};

		function pivotTable() {
			var utils = $.pivotUtilities;
			var dateFormat = utils.derivers.dateFormat;
			var sum = utils.aggregators["Sum"];

			var nrecoPivotExt = new NRecoPivotTableExtensions({
				fixedHeaders: true,
				drillDownHandler: function(dataFilter) {
					var filtered = $filter("filter")(vm.model.monthDetails, {
						Date: dataFilter.Date,
						PID: dataFilter.PID,
						SPID: dataFilter.SPID,
						Task: dataFilter.Task
					}, true);
					vm.drillDownDate = dataFilter["Date"];
					vm.drillDownProcess = dataFilter["Process"];
					vm.drillDownSubProcess = dataFilter["Subprocess"];
					vm.drillDownTaskGroup = dataFilter["Task Group"];
					vm.drillDownTask = dataFilter["Task"];
					angular.copy(filtered, vm.records);
					vm.getStatistics(vm.selectedMonth);
				}
			});

			$("#pivotTable").pivot(vm.model.monthDetails, {
				renderer: nrecoPivotExt.wrapTableRenderer($.pivotUtilities.renderers["Table"]),
				rows: ["PID", "SPID", "Process", "Subprocess", "Task Group", "Task"],
				cols: ["Date", "Day of Week"],
				aggregator: sum(["Hours"]),
				derivedAttributes: {
					"Date": dateFormat("date", "%n-%d", true),
					"Day of Week": dateFormat("date", "%w", true)
				}
			});

			// Hack to hide empty row, known issue when user change the sort
			$("#pivotTable tbody > tr:first-child").addClass("pvtHiddenRow");

			// Init nreco pivot ext
			nrecoPivotExt.initFixedHeaders($("#pivotTable").find("table.pvtTable"));
		}

		vm.getStatistics = function(month) {
			return $http.get("/api/statistics/" + vm.owner + "/" + month)
				.then(function(response) {
					angular.copy(response.data, vm.model);
					var monthDetails = response.data.monthDetails.map(function(item) {
						return {
							"id": item.id,
							"date": item.date,
							"uriTag": item.uriTag,
							"PID": item.pid,
							"SPID": item.spid,
							"Process": item.processName,
							"Subprocess": item.subProcessName,
							"Task Group": item.location,
							"Task": item.task,
							"Subtask": item.subTask,
							"Hours": item.hours,
							"Description": item.description
						};
					});
					angular.copy(monthDetails, vm.model.monthDetails);
					pivotTable();
				});
		};

		vm.getProcessURIs = function() {
			return $http.get("/api/process/uris")
				.then(function (response) {
					angular.copy(response.data, vm.uriTags);
				});
		}

		vm.changeMonth = function(month) {
			$location.search("month", month);
			vm.getStatistics(vm.params.month);
			vm.records = [];
		};

		vm.getMonths();
		vm.getProcessURIs();
	}

})();