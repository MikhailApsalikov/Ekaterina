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
						return false;
					}
					return true;
				});
			}
		};
	}
})();