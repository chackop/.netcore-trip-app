(function() {
    "use strict";
    angular.module("app-trips")
    .controller("tripsController", tripsController);

    function tripsController($http) {
        var vm = this;
        vm.trips = [{
            name: "US trip",
            created: new Date()
        },
        {
            name: "World trip",
            created: new Date()
        }];

        vm.newTrip = {};
        vm.isBusy = true;
        vm.errorMessage = "";

        $http.get("/api/trips")
        .then(function (response) {
            angular.copy(response.data, vm.trips);
            vm.isBusy = false;
        }, function() {
            vm.errorMessage = "Failed to load data" + error;
        })
        .finally(function() {
            vm.isBusy = false;
        });

        vm.addTrip = function () {
            vm.isBusy = true;
            vm.errorMessage = "";
            $http.post("api/trips", vm.newTrip)
            .then(function(response) {
                vm.trips.push(response.data);
                vm.newTrip = {};
            }, function() {
                vm.errorMessage = "failed to save new trip";
            })
            .finally(function() {
                vm.isBusy = false;
            })
            ;
        };
    }


})();