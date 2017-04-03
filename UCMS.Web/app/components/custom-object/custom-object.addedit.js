var module = angular.module('uccm.app.cobject.addedit');

module.controller('cObjAddEditCtrl', function($scope, $http) {
    $scope.objDef = {};

    $scope.objItem = {};

    $scope.save = function() {
        $http.post('/api/object/insert/' + $scope.objDef.Name, $scope.objItem).then(function(response) {
            $scope.cancel();
        });
    }

    $scope.cancel = function() {
        location.href = "/o/" + $scope.objDef.Name + '/list';
    }

    $scope.init = function () {
        var pathName = location.pathname;
        var objName = pathName.split("/")[2];

        $http.get('/api/object/describe/' + objName).then(function (response) {
            $scope.objDef = response.data;
        });
    }
});