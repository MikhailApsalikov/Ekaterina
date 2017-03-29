(function () {
	"use strict";
	var angular = window.angular;
	angular
		.module("APP")
		.service("errorService", ['$uibModal', errorService]);

	function errorService($uibModal) {
		return {
			open: function (errors, then) {
				var modalInstance = $uibModal.open({
					ariaLabelledBy: 'modal-title',
					ariaDescribedBy: 'modal-body',
					templateUrl: 'error.html',
					controller: 'errorController',
					resolve: {
						errorsData: function () {
							return errors;
						}
					}
				});

				modalInstance.result.then(then, function() {});
			}
		};
	}
})();