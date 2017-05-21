(function() {
	"use strict";
	var angular = window.angular;
	angular
		.module("APP")
		.controller("subjectEditController",
			["$scope", "baseService", "loginService", "messageService", subjectEditController]);

	function subjectEditController($scope, service, loginService, messageService) {
		$scope.focs = {};
		$scope.modules = {};
		$scope.subject = {};
		$scope.userRole = loginService.roles[loginService.getUserInfo().Role];
		$scope.isPageVisible = function() {
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
				service.getById("subject", $scope.getId()).then(function(data) {
					if (data && data.FormsOfControl && data.FormsOfControl.length) {
						$scope.focs.test = !!_.find(data.FormsOfControl,
							function(item) {
								return item.Id === "http://localhost:3030/25";
							});
						$scope.focs.exam = !!_.find(data.FormsOfControl,
							function(item) {
								return item.Id === "http://localhost:3030/228";
							});
					}

					if (data && data.Modules && data.Modules.length) {
						$scope.modules.first = !!_.find(data.Modules,
							function (item) {
								return item.Id === "http://www.semanticweb.org/12/ontologies/2016/10/untitled-ontology-170#301";
							});
						$scope.modules.second = !!_.find(data.Modules,
							function (item) {
								return item.Id === "http://www.semanticweb.org/12/ontologies/2016/10/untitled-ontology-170#302";
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

			if (!$scope.modules.first && !$scope.modules.second) {
				messageService.error("Дисциплина должна проходить хотя бы в одном семестре");
				return;
			}

			$scope.subject.FormsOfControl = [];
			if ($scope.focs.test) {
				$scope.subject.FormsOfControl.push("http://localhost:3030/25");
			}
			if ($scope.focs.exam) {
				$scope.subject.FormsOfControl.push("http://localhost:3030/228");
			}

			$scope.subject.Modules = [];
			if ($scope.modules.first) {
				$scope.subject.Modules.push("http://www.semanticweb.org/12/ontologies/2016/10/untitled-ontology-170#301");
			}
			if ($scope.modules.second) {
				$scope.subject.Modules.push("http://www.semanticweb.org/12/ontologies/2016/10/untitled-ontology-170#302");
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