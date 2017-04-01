(function () {
	'use strict';
	var angular = window.angular;
	angular
      .module('APP')
      .controller('userManagementController', ['$scope', 'loginService', 'baseService', userManagementController]);

	function userManagementController($scope, loginService, service) {
		var roles = [
			'Пользователь',
			'Эксперт',
			'Администратор'
		];

		$scope.role = null;
		$scope.data = [];

		$scope.isPageVisible = function () {
			return $scope.role === roles[2];
		};

		$scope.reload = function () {
			service.getList("account").then(function (data) {
				$scope.data = data.Data;
			});
		};

		$scope.userRoleString = function (role) {
			return roles[role];
		};

		$scope.up = function (account) {
			account.Role = account.Role + 1;
			service.update("account", account.Id, account).then(function () {
				$scope.reload();
			});
		};

		$scope.down = function (account) {
			account.Role = account.Role - 1;
			service.update("account", account.Id, account).then(function () {
				$scope.reload();
			});
		};

		$scope.remove = function (id) {
			service.remove("account", id).then(function () {
				$scope.reload();
			});
		};

		function updateUserInfo() {
			var userInfo = loginService.getUserInfo();
			$scope.userName = userInfo.Name;
			$scope.role = roles[userInfo.Role];
		}

		function activate() {
			updateUserInfo();
			$scope.reload();
		};

		activate();
	}
})();