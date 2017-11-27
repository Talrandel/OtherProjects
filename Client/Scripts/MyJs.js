var myApp = angular.module('myApp', []);
myApp.controller("personsController", function ($scope, $http, $filter) {

    $scope.loaded = false;
    $scope.currentPage = 0;
    $scope.pageSize = 5;
    $scope.persons = [];
    $scope.index = '';

    $scope.numberOfPages = function () {
        return Math.ceil($scope.persons.length / $scope.pageSize);
    };
    $scope.getData = function () {
        return $filter('filter')($scope.persons, $scope.index)
    };

    $scope.getFullName = function (p) {
        return p.firstName + ' ' + p.lastName + ' ' + p.patronicName;
    };


    $scope.load = function () {
        $scope.sortparam = 'id';
        $scope.persons = [{
            id: 1,
            firstName: 'first1',
            lastName: 'last1',
            patronicName: 'patr1',
            contacts: ['contact1', 'contact2']
        }, {
            id: 2,
            firstName: 'first2',
            lastName: 'last2',
            patronicName: 'patr2',
            contacts: ['contact3', 'contact4']
        }, {
            id: 3,
            firstName: 'first3',
            lastName: 'last3',
            patronicName: 'patr3',
            contacts: ['contact5', 'contact6']
        }, {
            id: 5,
            firstName: 'first5',
            lastName: 'last5',
            patronicName: 'patr5',
            contacts: ['contact5', 'contact6']
        }, {
            id: 6,
            firstName: 'first6',
            lastName: 'last6',
            patronicName: 'patr6',
            contacts: ['contact5', 'contact6']
        }, {
            id: 4,
            firstName: 'first4',
            lastName: 'last4',
            patronicName: 'patr4',
            contacts: ['contact5', 'contact6']
        }, ];
        $scope.pageCount = Math.ceil($scope.persons.length / $scope.limit);
        $scope.loaded = true;
        for (var i = 0; i < $scope.persons.length; i++) {
            $scope.persons[i].fullName = $scope.persons[i].firstName + ' ' + $scope.persons[i].lastName + ' ' + $scope.persons[i].patronicName;
        };
    };

    //$scope.load2 = $http.get('http://localhost:3311/api/Persons').
    //then(function success(response) {
    //    $scope.sortparam = 'id';

    //    $scope.persons = response.data.persons;

    //    $scope.pageCount = Math.ceil($scope.persons.length / $scope.limit);
    //    $scope.loaded = true;
    //    for (var i = 0; i < $scope.persons.length; i++) {
    //        $scope.persons[i].fullName = $scope.persons[i].firstName + ' ' + $scope.persons[i].lastName + ' ' + $scope.persons[i].patronicName;
    //    };
    //});
    
});

myApp.filter('startFrom', function () {
    return function (input, start) {
        start = +start; //parse to int
        return input.slice(start);
    }
});