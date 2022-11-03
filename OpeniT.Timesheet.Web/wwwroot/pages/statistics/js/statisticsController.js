// statisticsController.js

(function() {
	"use strict";

	angular.module("app-statistics").controller("statisticsController", statisticsController);

	function statisticsController($scope, $q, $http, $routeParams, $filter, $location, $uibModal, Notification) {

		var vm = this;

		// initialize variables
		vm.model = {};
		vm.users = [];
		vm.usersElapsed = {};
		vm.currentDate = new Date();
		vm.currentPage = 1;
		vm.itemsPerPage = 20;
		vm.isHR = $routeParams.access === "hr";

		vm.usersTable = [];
		vm.usersAll = [];
		vm.showAll = false;
		vm.loading = true;

		vm.timesheetSiteValues;
		vm.timesheetDecemberSubmissionStartKey = "TimesheetDecemberSubmissionStart";

		// Initialize                                           
		$scope.$on("$viewContentLoaded", function () {
			$q.all([vm.getStatistics($routeParams.owner, $routeParams.month), vm.getSiteValues(), vm.getUsers(), vm.getAllUsers()])
				.then(function (responses) {
					vm.loading = false;
					vm.usersTable = vm.users;
				});
		});

		vm.getStatistics = function(owner, month) {
			$location.search("owner", owner);
			$location.search("month", month);

			return $http.get("/api/statistics", {
				params: {
					owner: owner,
					month: month
				}
			}).then(function(response) {
				angular.copy(response.data, vm.model); 
				console.log(vm.model);

				vm.selectedYear = vm.model.monthlySummary.length ? parseInt($filter("date")(vm.model.monthlySummary[0].month, "yyyy")) : vm.model.distinctYears[vm.model.distinctYears.length - 1];				
				vm.excessHours = $filter("filter")(vm.model.excessHours, { year: vm.selectedYear - 1 });
				vm.excessVL = $filter("filter")(vm.model.excessVL, { year: vm.selectedYear })[0];

				if (vm.model.monthlySummary.length >= 2) {
					vm.udpatedTO = vm.model.monthlySummary[vm.model.monthlySummary.length - 2].excessHours;
				}
			}, function() {
				console.log("Failed to get statistics");
			});
		};

		vm.getUsers = function() {
			return $http.get("/api/users/active-no-status")
				.then(function(response) {
					angular.copy(response.data.results, vm.users);
					vm.usersElapsed = response.data.elapsed;
				}, function() {
					console.log("Failed to get all users");
				});
		};

		vm.getAllUsers = function () {
			return $http.get("/api/users/all-no-status")
				.then(function (response) {
					angular.copy(response.data.results, vm.usersAll)
				}, function () {
					console.log("Failed to get all users");
				});
		};

		vm.getSiteValues = function () {
			var path = "/api/sitevalues/multiple";
			path += $filter("stringArrayToQueryString")("keys", [vm.timesheetDecemberSubmissionStartKey]);

			return $http.get(path)
				.then(function (response) {
					vm.timesheetSiteValues = $filter("convertSiteValuesArrayToModel")(response.data);
				}, function () {
					console.error("Failed to get site values");
				});
		};

		vm.toggleExcessVL = function(year) {
			$http.put("/api/users/manage/" + vm.model.email + "/toggle/excessvl?year=" + year)
				.then(function() {
				}, function() {
					Notification.error("Toggle failed, please try again!");
				});
		};

		vm.submit = function(data) {
			// Open a modal dialog to confirm submission
			var dialog = {};
			dialog.title = "Lock Timesheet";
			dialog.message = "In submission of your Timesheet, you are confirming that you have reviewed the details and confirm its accuracy.";
			dialog.cancel = "Cancel";
			dialog.confirm = "Submit";

			var modalInstance = $uibModal.open({
				animation: true,
				ariaLabelledBy: "modal-title",
				ariaDescribedBy: "modal-body",
				component: "confirmDialogComponent",
				controller: "confirmDialogComponent",
				controllerAs: "vm",
				size: "sm",
				resolve: {
					dialog: function() {
						return dialog;
					}
				}
			});

			// Now let's see what the user want's to do
			modalInstance.result.then(function(action) {
				// Take action based on user's selection
				if (action === dialog.confirm) {
					vm.isBusy = true;
					var path;

					if (!$routeParams.owner) {
						path = "/api/statistics";
					} else {
						path = "/api/statistics/" + $routeParams.owner;
					}

					$http.post(path, data)
						.then(function () {
							vm.getStatistics($routeParams.owner, $routeParams.month)
								.then(function () {
									Notification.success("Cheers! Timesheet for '" + $filter('date')(data.month, 'yyyy-MMMM') + "' has been submitted!");
								}, function (response) {
									Notification.error(response.data);
								});
						}, function (response) {
							Notification.error(response.data);
						}).finally(function () {
							vm.isBusy = false;
						});
				}
			}, function() {
				console.log("modal-component dismissed at: " + new Date());
			});
		};

		vm.unlock = function (data) {
			vm.isBusy = true;
			var path;

			var summary = $filter("filter")(vm.model.recordSummary, { month: data.month })[0];

			if (!$routeParams.owner) {
				path = "/api/statistics/" + summary.id;
			} else {
				path = "/api/statistics/" + summary.id + "/" + $routeParams.owner;
			}

			$http.delete(path)
				.then(function () {
				}).finally(function () {
					var summaryIndex = vm.model.recordSummary.indexOf(summary);
					vm.model.recordSummary.splice(summaryIndex, 1);

					data.submittedOn = null;

					vm.getStatistics($routeParams.owner, $routeParams.month)
						.then(function () {
							Notification.success("Unlocked!");
						}, function (response) {
							Notification.error(response.data);
						});
				}).finally(function () {
					vm.isBusy = false;
				});
		};

		vm.recalculate = function (data) {
			vm.isBusy = true;
			var path;

			var summary = $filter("filter")(vm.model.recordSummary, { month: data.month })[0];

			path = "/api/statistics/recalculate/" + summary.id;

			$http.put(path)
				.then(function () {
					vm.getStatistics($routeParams.owner, $routeParams.month)
						.then(function () {
							Notification.success("Timesheet recalculated successfully!");
						}, function (response) {
							Notification.error(response.data);
						});
				}, function (response) {
					Notification.error(response.data);
				}).finally(function () {
					vm.isBusy = false;
				});
		};

		vm.isSubmitAllowed = function (month) {
			var now = new Date();
			var monthNow = now.getMonth();
			var date = now.getDate();
			var december = 11;
			var enableDate;

			if (vm.timesheetSiteValues) {
				var enableDate = new Date(vm.timesheetSiteValues[vm.timesheetDecemberSubmissionStartKey]).getDate();
			} else {
				enableDate = 26;
			}

			if (monthNow == december && date >= enableDate) {
				return true;
			}

			var currentMonth = new Date(month);
			var lastDay = new Date(currentMonth.getFullYear(), currentMonth.getMonth() + 1, 0);

			return vm.currentDate >= lastDay;
		};

		vm.filterOnChange = function () {
			if (vm.showAll) {
				vm.usersTable = vm.usersAll;
			} else {
				vm.usersTable = vm.users;
			}
		};

		vm.clearFilters = function () {
			vm.showAll = false;

			vm.filterOnChange();
		};
	}
})();