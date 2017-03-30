(function () {
	'use strict';
	var angular = window.angular;
	angular
      .module('APP')
      .controller('userOptionsController', ['$scope', 'loginService', userOptionsController]);

	function userOptionsController($scope, loginService) {
		var roles = [
			'Пользователь',
			'Эксперт',
			'Администратор'
		];

		$scope.userName = null;
		$scope.role = null;
		$scope.isLoggedIn = function () {
			return !!$scope.userName;
		};

		$scope.logout = loginService.logout;
		$scope.isAdmin = function() {
			return $scope.role === roles[2];
		};

		function updateUserInfo() {
			var userInfo = loginService.getUserInfo();
			$scope.userName = userInfo.Name;
			$scope.role = roles[userInfo.Role];
		}

		function activate() {
			loginService.checkIfLoggedIn();
			updateUserInfo();
			
		};

		activate();
	}
})();