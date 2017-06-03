(function() {
	"use strict";
	var angular = window.angular;
	angular
		.module("APP")
		.controller("studyProgrammeController",
			["$scope", "baseService", "loginService", "messageService", studyProgrammeController]);

	function studyProgrammeController($scope, service, loginService, messageService) {
		$scope.directions = [];
		$scope.departments = [];
		$scope.profiles = [];
		$scope.data = [];
		$scope.userRole = loginService.roles[loginService.getUserInfo().Role];
		$scope.isEditable = function() {
			return $scope.userRole !== loginService.roles[0];
		};

		$scope.getId = function() {
			if ($scope.id) {
				return $scope.id;
			}
			try {
				return /\#id=(.*)/.exec(window.location.hash)[1];
			} catch (e) {
			}
		};

		$scope.reload = function() {
			if ($scope.getId()) {
				service.getById("studyProgramme", $scope.getId()).then(function(data) {
					$scope.data = data;
				});
			}
		};

		$scope.loadSelectData = function () {
			service.getById("direction").then(function (data) {
				$scope.directions = data;
			});
			service.getById("departments").then(function (data) {
				$scope.departments = data;
			});
			service.getById("profiles").then(function (data) {
				$scope.profiles = data;
			});
		};

		$scope.save = function() {
			if (!$scope.data.Title) {
				messageService.error("Введите название учебного плана");
				return;
			}

			if ($scope.getId()) {
				service.update("studyProgramme", $scope.getId(), $scope.data).then(function() {
					messageService.success("Учебный план успешно сохранен в онтологию");
				});
			} else {
				service.create("studyProgramme", $scope.data).then(function(data) {
					$scope.id = data.ModifiedEntity.Id;
					messageService.success("Учебный план успешно создана и сохранена в онтологии");
				});
			}
		};

		function activate() {
			if ($scope.isEditable()) {
				$scope.loadSelectData();
			}
			
			$scope.reload();
		}

		activate();
	}
})();