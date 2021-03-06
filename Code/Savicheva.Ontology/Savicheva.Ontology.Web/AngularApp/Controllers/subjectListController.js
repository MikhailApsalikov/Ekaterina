﻿(function () {
	'use strict';
	var angular = window.angular;
	angular
      .module('APP')
		.controller('subjectListController', ['$scope', 'baseService', 'loginService', 'messageService', subjectListController]);

	function subjectListController($scope, service, loginService, messageService) {
		$scope.subjects = [];
		$scope.userRole = loginService.getUserInfo().Role;
		$scope.canEdit = function() {
			return $scope.userRole !== 0;
		};
		$scope.filters = {
			Title: "",
			FormsOfControl: null,
			StudyProgramme: "",
			HasHourForPract: null,
			HasHourForLecture: null,
			HasHourForLab: null,
			HasHourForKoll: null,
			HasHourForInd: null
		};
		$scope.formsOfControl = [
			{
				id: null,
				name: "---------------"
			},
			{
				id: "http://localhost:3030/25",
				name: "Зачет"
			},
			{
				id: "http://localhost:3030/228",
				name: "Экзамен"
			}
		];

		$scope.modules = [
			{
				id: null,
				name: "---------------"
			},
			{
				id: "http://www.semanticweb.org/12/ontologies/2016/10/untitled-ontology-170#301",
				name: "1 семестр"
			},
			{
				id: "http://www.semanticweb.org/12/ontologies/2016/10/untitled-ontology-170#302",
				name: "2 семестр"
			}
		];

		$scope.reload = function() {
			service.getList("subject", $scope.filters).then(function(data) {
				$scope.subjects = data.Data;
			});
		};

		$scope.formatModules = function (modules) {
			var names = [];
			angular.forEach(modules, function (module) {
				names.push(module.Title);
			});

			return names.join(", ");
		};

		$scope.formatFormsOfControl = function(focs) {
			var focNames = [];
			angular.forEach(focs, function(foc) {
				focNames.push(foc.Title);
			});

			return focNames.join(", ");
		};

		$scope.remove = function(id) {
			service.remove("subject", id).then(function () {
				messageService.success("Дисциплина успешно удалена из онтологии");
				$scope.reload();
			});
		};

		function activate() {
			$scope.reload();
		}
		activate();
	}
})();