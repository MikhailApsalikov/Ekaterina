(function() {
	"use strict";
	var angular = window.angular;
	angular
		.module("APP")
		.controller("subjectEditController",
			["$scope", "baseService", "loginService", "messageService", subjectEditController]);

	function subjectEditController($scope, service, loginService, messageService) {
		$scope.focs = {};
		$scope.subject = {};
		$scope.userRole = loginService.roles[loginService.getUserInfo().Role];
		$scope.isPageVisible = function() {
			return $scope.userRole !== loginService.roles[0];
		};

		$scope.getId = function() {
			if ($scope.id) {
				return $scope.id;
			}
			var url = window.location.href.split("/");
			return +url[url.length - 1];
		};
		$scope.reload = function() {
			if ($scope.getId()) {
				service.getById("subject", $scope.getId()).then(function(data) {
					if (data && data.FormsOfControl && data.FormsOfControl.length) {
						$scope.focs.test = !!_.find(data.FormsOfControl,
							function(item) {
								return item.Id === 25;
							});
						$scope.focs.exam = !!_.find(data.FormsOfControl,
							function(item) {
								return item.Id === 228;
							});
					}

					$scope.subject = data;
				});
			}
		};

		$scope.save = function() {
			if (!$scope.subject.Title) {
				messageService.error("Введите название дисциплины");
				return;
			}

			if (!$scope.focs.test && !$scope.focs.exam) {
				messageService.error("Дисциплина должна иметь хотя бы одну форму контроля");
				return;
			}

			$scope.subject.FormsOfControl = [];
			if ($scope.focs.test) {
				$scope.subject.FormsOfControl.push(25);
			}
			if ($scope.focs.exam) {
				$scope.subject.FormsOfControl.push(228);
			}

			if ($scope.getId()) {
				service.update("subject", $scope.getId(), $scope.subject).then(function() {
					messageService.success("Дисциплина успешно сохранена в онтологию");
				});
			} else {
				service.create("subject", $scope.subject).then(function(data) {
					$scope.id = data.ModifiedEntity.Id;
					messageService.success("Дисциплина успешно создана и сохранена в онтологии");
				});
			}
		};

		function activate() {
			$scope.reload();
		}

		activate();
	}
})();