(function () {
	"use strict";
	var angular = window.angular;
	angular
		.module("APP")
		.service("messageService", ['$rootScope', messageService]);

	function messageService($rootScope) {
		var duration = 5000;

		return {
			error: function (text) {
				$rootScope.errorMessage = text;
				window.setTimeout(function () {
					$rootScope.errorMessage = null;
					$rootScope.$apply();
				},
					duration);
			},
			success: function (text) {
				$rootScope.successMessage = text;
				window.setTimeout(function () {
					$rootScope.successMessage = null;
					$rootScope.$apply();
				},
					duration);
			}
		};
	}
})();