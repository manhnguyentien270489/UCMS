var module = angular.module('uccm.app.cform.list');

module.controller('cformCtrl', function($scope, $http, $uibModal) {

    $scope.cforms = [];

    $scope.createNew = function() {
        var modal = $uibModal.open({
            controller: 'newFormModalCtrl',
            templateUrl: 'newFormModal.html',
        });

        modal.result.then(function(cform) {
            $http.post('/api/cform/insert', cform).then(function() {
                $scope.init();
            });
        });
    }

    $scope.edit = function(form) {
        var url = '/form/edit?id=' + form.Id;
        location.href = url;
    }

    $scope.delete = function(form) {
        
    }

    $scope.init = function() {
        $http.get('/api/cform/getforms').then(function(response) {
            $scope.cforms = response.data;
        });
    }

});

module.controller('newFormModalCtrl', function($scope, $uibModalInstance) {

    $scope.cform = {
        Name: '',
        Title: '',
        Description:''
    }

    $scope.ok = function() {
        $uibModalInstance.close($scope.cform);
    }

    $scope.cancel = function() {
        $uibModalInstance.dismiss('cancel');
    }

})