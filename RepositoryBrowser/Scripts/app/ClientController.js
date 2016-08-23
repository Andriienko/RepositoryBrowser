'use strict';
var myApp = angular.module('myApp', []);
myApp.controller("RepositController", function ($scope, $http) {
    
    var GetDrives = function () {
        $http.get('http://localhost:52072/api/HDD').success(function (response) {
            $scope.drives = response;
        })
         .error(function (response) {
             $scope.drives = response;
         });
    };
    var GetDirectories = function () {
        $http.get('http://localhost:52072/api/Repository').success(function (response) {
            $scope.res = response;
        });
    };
    var GetFiles = function () {
        $http.get('http://localhost:52072/api/Files').success(function (response) {
            $scope.files = response;
        });
    };
    var GetCount = function () {
        $http.get('http://localhost:52072/api/Count').success(function (response) {
            $scope.count = [];
            $scope.count = response;
        });
    };
    var GetDirForAdressLine = function () {
        $http.get('http://localhost:52072/api/Files?val=1').success(function (data) {
            $scope.adres = data;
        });
    };
    GetDrives();
    GetDirectories();
    GetFiles();
    GetCount();
    GetDirForAdressLine();
    
    var GoToLink = function (data)
    {
        $scope.files = [];
        var Do = {
            dir: data
        }
        $http({
            method: 'POST',
            data: Do,
            url: 'http://localhost:52072/api/Repository/'
        }).success(function (data) {
            $scope.res = data;
            GetFiles();
            GetCount();
            GetDirForAdressLine();
        }).error(function (data) {
            $scope.res = data;
        });
    };
    $scope.Back = function () {
        $http.get('http://localhost:52072/api/Repository?value=1').success(function (data) {
            $scope.res = data;
            GetFiles();
            GetCount();
            GetDirForAdressLine();
        });
    };
    $scope.Adr = function (ref) { GoToLink(ref);}
    
});
