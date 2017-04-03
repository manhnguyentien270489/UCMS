var module = angular.module('uccm.app.object.list');

module.controller('objectListCtrl', function($scope, $http, $uibModal) {
    $scope.objects = [];

    $scope.createNew = function() {
        var modal = $uibModal.open({
            controller: 'newObjectModalCtrl',
            templateUrl: 'customObjectModal.html'
        });

        modal.result.then(function(obj) {
            $http.post('/api/metadata/createobject', obj).then(function() {
                refreshList();
            });
        });
    }

    $scope.manage = function(obj) {
        location.href = '/o/' + obj.Name + '/list';
    }

    function refreshList() {
        $http.get('/api/metadata/getobjects')
            .then(function (response) {
                $scope.objects = response.data;
            });
    }

    $scope.init = function() {
        refreshList();
    }
});

module.controller('newObjectModalCtrl', function($scope, $uibModalInstance) {
    $scope.customObject = {}

    $scope.ok = function () {
        $uibModalInstance.close($scope.customObject);
    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }
});