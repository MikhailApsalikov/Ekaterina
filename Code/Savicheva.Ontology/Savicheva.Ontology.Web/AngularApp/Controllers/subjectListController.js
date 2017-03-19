(function () {
	'use strict';
	var angular = window.angular;
	angular
      .module('APP')
      .controller('subjectListController', ['$scope', 'baseService', subjectListController]);

	function subjectListController($scope, service) {
		$scope.subjects = [];

		$scope.reload = function() {
			service.getList("subject").then(function(data) {
				$scope.subjects = data.Data;
			});
		};

		$scope.formatFormsOfControl = function(focs) {
			var focNames = [];
			angular.forEach(focs, function(foc) {
				focNames.push(foc.Title);
			});
			
			return focNames.join(", ");
		}

		function activate() {
			$scope.reload();
		}
		activate();
	}
})();