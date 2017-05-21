(function() {
	"use strict";
	var angular = window.angular;
	angular
		.module("APP")
		.controller("uploadController",
			["$scope", "loginService", "baseService", "messageService", uploadController]);

	function uploadController($scope, loginService, baseService, messageService) {
		$scope.role = null;

		$scope.isPageVisible = function() {
			return $scope.role === loginService.roles[1];
		};

		$scope.uploadFile = function(files) {
			baseService.uploadFile(files[0], "/api/rdf/import").then(function(data) {
					messageService.success("Файл успешно загружен! Загружено " + data.data + " RDF-триплетов");
				},
				function(data) {
					messageService.error("Ошибка при загрузке файла: " + data.data.Message);
				});
		};

		function updateUserInfo() {
			var userInfo = loginService.getUserInfo();
			$scope.userName = userInfo.Name;
			$scope.role = loginService.roles[userInfo.Role];
		}

		function activate() {
			updateUserInfo();
		};

		activate();
	}
})();