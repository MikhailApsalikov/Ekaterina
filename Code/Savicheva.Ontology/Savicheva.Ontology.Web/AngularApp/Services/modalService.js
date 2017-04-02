(function () {
	"use strict";
	var angular = window.angular;
	angular
		.module("APP")
		.service("modalService", ['$uibModal', modalService]);

	function modalService($uibModal) {
		return {
			openCustom: function (data, templateName, controllerName, then, cancel) {
				var modalInstance = $uibModal.open({
					ariaLabelledBy: 'modal-title',
					ariaDescribedBy: 'modal-body',
					templateUrl: templateName,
					controller: controllerName,
					resolve: {
						data: function () {
							return data;
						}
					}
				});

				modalInstance.result.then(then, cancel ? cancel : function () { });
			}
		};
	}
})();