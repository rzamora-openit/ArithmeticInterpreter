// confirmDialogComponent.js

(function () {
	"use strict";

	angular.module("simpleControls").component("confirmDialogComponent", {
		templateUrl: "/pages/site/views/confirmDialogContent.html",
		bindings: {
			resolve: "<",
			close: "&",
			dismiss: "&"
		},
		controller: function() {
			var vm = this;

			vm.$onInit = function() {
				vm.dialog = vm.resolve.dialog;
			};

			vm.cancel = function() {
				vm.close({ $value: vm.dialog.cancel });
			};

			vm.confirm = function() {
				vm.close({ $value: vm.dialog.confirm });
			};
		}
	});
})();