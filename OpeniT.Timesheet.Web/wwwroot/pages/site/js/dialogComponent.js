// dialogComponent.js

(function () {
	"use strict";

	angular.module("simpleControls").component("dialogComponent", {
		templateUrl: "/pages/site/views/dialogContent.html",
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
		}
	});
})();