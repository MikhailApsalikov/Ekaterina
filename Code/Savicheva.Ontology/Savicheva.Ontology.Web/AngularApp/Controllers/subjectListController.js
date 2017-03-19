(function () {
	'use strict';

	angular
      .module('APP')
      .controller('subjectListController', ['$scope', 'baseService', subjectListController]);

	function subjectListController($scope, service) {
		$scope.data = [];

		$scope.reload = function() {
			service.getList("subject").then(function(data) {
				$scope.data = data;
			});
		};

		function activate() {
			$scope.reload();
		}
		activate();
	}
})();