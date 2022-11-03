// statisticsYearContoller.js

(function() {
	"use strict";

	angular.module("app-statistics").controller("statisticsYearController", statisticsYearController);

	function statisticsYearController($scope, $http, $filter, $routeParams, $location) {
		var vm = this;

		vm.owner = $routeParams.owner;
		vm.params = $location.search();
		vm.year = vm.params.year == undefined ? new Date().getFullYear() : vm.params.year;
		vm.model = {};

		function pivotTable() {
			var utils = $.pivotUtilities;
			var dateFormat = utils.derivers.dateFormat;
			var sum = utils.aggregators["Sum"];
			var sortAs = utils.sortAs;

			var nrecoPivotExt = new NRecoPivotTableExtensions({
				fixedHeaders: true
			});

			$("#pivotTable").pivot(vm.model.monthDetails, {
				renderer: nrecoPivotExt.wrapTableRenderer($.pivotUtilities.renderers["Table"]),
				rows: ["PID", "SPID", "Process", "Subprocess", "Task Group", "Task"],
				cols: ["Month"],
				aggregator: sum(["Hours"]),
				derivedAttributes: {
					"Month": dateFormat("date", "%n", false)
				},
				sorters: {
					"Month": sortAs(["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"])
				}
			});

			nrecoPivotExt.initFixedHeaders($("#pivotTable").find('table.pvtTable'));
		}

		vm.getStatistics = function(year) {
			return $http.get("/api/statistics/" + vm.owner + "/year/" + year)
				.then(function(response) {
					angular.copy(response.data, vm.model);
					var yearSummary = response.data.monthDetails.map(function (item) {
						return {
							"date": item.date,
							"PID": item.pid,
							"SPID": item.spid,
							"Process": item.processName,
							"Subprocess": item.subProcessName,
							"Task Group": item.location,
							"Task": item.task,
							"Hours": item.hours
						};
					});
					angular.copy(yearSummary, vm.model.monthDetails);
					pivotTable();
				});
		};

		vm.changeMonth = function(year) {
			$location.search("year", year);
			vm.getStatistics(vm.params.year);
		};

		vm.getStatistics(vm.year);
	}

})();