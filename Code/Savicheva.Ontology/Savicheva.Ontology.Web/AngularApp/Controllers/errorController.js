(function () {
	'use strict';
	var angular = window.angular;
	angular
      .module('APP')
      .controller('errorController', ['$scope', '$uibModalInstance', 'errorsData', errorController]);

	function errorController($scope, $uibModalInstance, errorsData) {
		$scope.errors = errorsData;
		$scope.ok = function () {
			$uibModalInstance.close();
		};

		function activate() {
		}

		activate();
	}
})();