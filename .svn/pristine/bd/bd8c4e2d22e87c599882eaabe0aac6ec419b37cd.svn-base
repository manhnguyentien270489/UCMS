var module = angular.module('uccm.app.object.detail');


module.controller('objDetailCtrl', function($scope, $http, $uibModal) {

    $scope.customObject = {};
    $scope.objects = [];

    $scope.addField = function() {
        var modal = $uibModal.open({
            controller: 'newFieldModalCtrl',
            templateUrl: 'newFieldModal.html',
            resolve: {
                objects: function() { return $scope.objects; }
            }
        });

        modal.result.then(function(field) {
            field.ObjectId = $scope.customObject.Id;

            $http.post('/api/metadata/createField', field).then(function(response) {
                refresh();
            });
        });
    }

    function refresh() {
        var objName = Utils.getParameterByName("name");
        $http.get('/api/object/describe/' + objName).then(function (response) {
            $scope.customObject = response.data;
        });
    }

    $scope.init = function() {
        refresh();

        $http.get('/api/metadata/getobjects').then(function(response) {
            $scope.objects = response.data;
        });
    }
});

module.controller('newFieldModalCtrl', function ($scope, $http, $uibModalInstance, objects) {

    $scope.field = {
        Id: '',
        Name: '',
        DataType: 'Text',
        Length: 255,
        LookupName: '',
        Items: []
    };

    $scope.dataTypeList = [
        'Text',
        'TextArea',
        'Checkbox',
        'Number',
        'Picklist',
        'MultiSelect',
        'Lookup'
    ];

    $scope.objects = objects;

    $scope.ok = function() {
        $uibModalInstance.close($scope.field);
    }

    $scope.cancel = function() {
        $uibModalInstance.cancel('dismiss');
    }
});