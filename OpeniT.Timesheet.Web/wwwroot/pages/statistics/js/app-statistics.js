// app-statistics.js

(function() {
	"use strict";

	angular.module("app-statistics", ["simpleControls", "ngRoute", "ui.bootstrap", "angular.filter", "smart-table", "ui.toggle", "ui-notification"])
		.config(function($routeProvider) {
			$routeProvider.when("/", {
				controller: "statisticsController",
				controllerAs: "vm",
				templateUrl: "/pages/statistics/views/statisticsView.html",
				reloadOnSearch: false
			});

			$routeProvider.when("/month/:owner", {
				controller: "statisticsMonthController",
				controllerAs: "vm",
				templateUrl: "/pages/statistics/views/statisticsMonthView.html",
				reloadOnSearch: false
			});

			$routeProvider.when("/year/:owner", {
				controller: "statisticsYearController",
				controllerAs: "vm",
				templateUrl: "/pages/statistics/views/statisticsYearView.html",
				reloadOnSearch: false
			});

			$routeProvider.otherwise({ redirectTo: "/" });
		})
		.config(["$httpProvider", function ($httpProvider) {
			// http://stackoverflow.com/questions/16098430/angular-ie-caching-issue-for-http
			//initialize get if not there
			if (!$httpProvider.defaults.headers.get) {
				$httpProvider.defaults.headers.get = {};
			}

			//disable IE ajax request caching
			$httpProvider.defaults.headers.get["If-Modified-Since"] = "Fri, 28 Jun 1985 05:00:00 GMT";
			// extra
			$httpProvider.defaults.headers.get["Cache-Control"] = "no-cache";
			$httpProvider.defaults.headers.get["Pragma"] = "no-cache";
		}]);

})();