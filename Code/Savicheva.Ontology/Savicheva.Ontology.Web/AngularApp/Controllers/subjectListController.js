(function () {
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
				id: 25,
				name: "Зачет"
			},
			{
				id: 228,
				name: "Экзамен"
			}
		];

		$scope.reload = function() {
			service.getList("subject", $scope.filters).then(function(data) {
				$scope.subjects = data.Data;
			});
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