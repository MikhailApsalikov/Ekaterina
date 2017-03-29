(function () {
	"use strict";
	var angular = window.angular;
	angular
		.module("APP")
		.service("loginService", ["$http", 'errorService', loginService]);

	function loginService($http, errorService) {
		return {
			login: function (loginData) {
				return $http({
					url: "api/user/login",
					method: "POST",
					data: loginData
				}).then(function (responce) {
					if (!responce.data.IsValid) {
						errorService.open(responce.data.Errors);
						return;
					}
					window.localStorage.user = JSON.stringify(responce.data.ModifiedEntity);
					window.location = "/";
				});
			},
			logout: function () {
				delete window.localStorage.user;
				if (!window.localStorage.user && window.location.pathname !== "/Login") {
					window.location = "/Login";
				}
			},
			getUserInfo: function () {
				if (window.localStorage.user) {
					return JSON.parse(window.localStorage.user);
				}
				return {};
			},
			checkIfLoggedIn: function () {
				if (!window.localStorage.user && window.location.pathname !== "/Login") {
					window.location = "/Login";
				}
			}
		};
	}
})();