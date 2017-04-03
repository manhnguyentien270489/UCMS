var module = angular.module('uccm.app.cobject.list');

module.controller('cobjectListCtrl', function($scope, $http) {
    $scope.cobjects = {
        Total: 0,
        Items: []
    };

    $scope.objDef = {};

    $scope.pageFitter = {
        pageIndex: 1,
        pageSize: 25
    }

    $scope.addNew = function() {
        location.href = "/o/" + $scope.objDef.Name + "/itemaddedit";
    }

    $scope.refresh = function() {
        var q = {
            PageIndex: $scope.pageFitter.pageIndex,
            PageSize: $scope.pageFitter.pageSize,
            Conditions: '',
            Fields: ['Id']
        };

        _.each($scope.objDef.Fields, function(f) {
            q.Fields.push(f.Name);
        });

        $http.post('/api/object/query/' + $scope.objDef.Name, q).then(function(response) {
            $scope.cobjects = response.data;
        });
    }

    $scope.init = function () {
        var pathName = location.pathname;
        var objName = pathName.split("/")[2];

        $http.get('/api/object/describe/' + objName).then(function(response) {
            $scope.objDef = response.data;
            $scope.refresh();
        });        
    }
});