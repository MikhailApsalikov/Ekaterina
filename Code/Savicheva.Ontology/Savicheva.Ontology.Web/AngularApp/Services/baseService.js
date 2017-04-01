(function () {
	"use strict";
	var angular = window.angular;
	angular
		.module("APP")
		.service("baseService", ["$http", 'errorService', baseService]);

	function baseService($http, errorService) {
		return {
			getList: function (controllerName, params) {
				return $http({
					url: "api/" + controllerName,
					method: "GET",
					params: params
				}).then(function (responce) {
					return responce.data;
				});
			},
			getById: function (controllerName, id) {
				return $http({
					url: "api/" + controllerName,
					method: "GET",
					params: {
						id: id
					}
				}).then(function (responce) {
					return responce.data;
				});
			},
			create: function (controllerName, entity) {
				entity = angular.copy(entity);
				return $http({
					url: "api/" + controllerName,
					method: "POST",
					data: entity
				}).then(function () {
					return responce.data;
				}, function (responce) {
					if (responce.data.isValid === false) {
						errorService.open(responce.data.errors);
						return;
					}
				});
			},
			update: function (controllerName, id, entity) {
				entity = angular.copy(entity);
				return $http({
					url: "api/" + controllerName,
					method: "PUT",
					data: entity,
					params: {
						id: id
					}
				}).then(function () {
					return responce.data;
				}, function (responce) {
					if (responce.data.isValid === false) {
						errorService.open(responce.data.errors);
						return;
					}
				});
			},
			remove: function (controllerName, id) {
				return $http({
					url: "api/" + controllerName,
					method: "DELETE",
					params: {
						id: id
					}
				});
			}
		};
	}
})();