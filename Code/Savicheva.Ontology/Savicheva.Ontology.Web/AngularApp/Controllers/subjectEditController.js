(function () {
	'use strict';
	var angular = window.angular;
	angular
		.module('APP')
		.controller('subjectEditController', ['$scope', 'baseService', 'loginService', subjectEditController]);

	function subjectEditController($scope, service, loginService) {
		$scope.subject = {};
		$scope.userRole = loginService.getUserInfo().Role;
		$scope.isPageVisible = function () {
			return $scope.userRole !== 0;
		};
		$scope.formsOfControl = [
			{
				id: 25,
				name: "Зачет"
			},
			{
				id: 228,
				name: "Экзамен"
			}
		];

		$scope.getId = function () {
			var url = window.location.href.split("/");
			return +url[url.length - 1];
		}

		$scope.reload = function () {
			if ($scope.getId()) {
				service.getById("subject", $scope.getId()).then(function (data) {
					$scope.subject = data.Data;
				});
			}
		};

		$scope.save = function () {
			console.log("Saved", $scope.subject);
		}

		function activate() {
			$scope.reload();
		}
		activate();
	}
})();