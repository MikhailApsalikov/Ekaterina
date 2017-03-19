(function () {
	"use strict";
	var angular = window.angular;
	angular
		.module("APP")
		.service("baseService", ["$http", policyService]);

	function policyService($http) {
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
				}).then(function(responce) {
					return responce.data;
				});
			},
			create: function (controllerName, entity) {
				entity = angular.copy(entity);
				return $http({
					url: "api/" + controllerName,
					method: "POST",
					data: entity
				}).then(function (responce) {
					return responce.data;
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
				}).then(function (responce) {
					return responce.data;
				});
			},
			remove: function(controllerName, id) {
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