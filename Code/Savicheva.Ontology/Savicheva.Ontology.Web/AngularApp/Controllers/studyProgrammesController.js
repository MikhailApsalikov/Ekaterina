(function () {
	'use strict';
	var angular = window.angular;
	angular
      .module('APP')
		.controller('studyProgrammesController', ['$scope', 'baseService', 'loginService', 'messageService', studyProgrammesController]);

	function studyProgrammesController($scope, service, loginService, messageService) {
		$scope.data = [];
		$scope.userRole = loginService.getUserInfo().Role;
		$scope.canEdit = function() {
			return $scope.userRole !== 0;
		};

		$scope.filters = {
			Title: "",
			Direction: "",
			Department: ""
		};

		$scope.reload = function() {
			service.getList("studyProgramme", $scope.filters).then(function(data) {
				$scope.data = data.Data;
			});
		};

		$scope.remove = function(id) {
			service.remove("studyProgramme", id).then(function () {
				messageService.success("Учебный план успешно удален из онтологии");
				$scope.reload();
			});
		};

		function activate() {
			$scope.reload();
		}
		activate();
	}
})();