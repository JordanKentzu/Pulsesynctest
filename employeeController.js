angular.module("employeeApp", [])
    .controller("employeeController", function ($scope, $http) {
        $scope.employees = [];
        $scope.youngestEmployee = null;
        $scope.eldestEmployee = null;

        $scope.loadFromFile = function () {
            
        };

        $scope.loadFromDatabase = function () {
            $http.get("/api/employees")
                .then(function (response) {
                    $scope.employees = response.data;
                });

            $http.get("/api/employees/youngest")
                .then(function (response) {
                    $scope.youngestEmployee = response.data;
                });

            $http.get("/api/employees/eldest")
                .then(function (response) {
                    $scope.eldestEmployee = response.data;
                });
        };
    });
