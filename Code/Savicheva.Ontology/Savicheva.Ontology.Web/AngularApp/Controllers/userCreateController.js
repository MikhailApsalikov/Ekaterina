(function () {
	'use strict';
	var angular = window.angular;
	angular
      .module('APP')
      .controller('userCreateController', ['$scope', '$uibModalInstance', 'data', userCreateController]);

	function userCreateController($scope, $uibModalInstance, data) {;
		$scope.cancel = function () {
			$uibModalInstance.dismiss();
		};

		$scope.ok = function () {
			$uibModalInstance.close({
				Name: $scope.login,
				Password: $scope.password,
				Role: 0
			});
		};

		function activate() {
		}

		activate();
	}
})();