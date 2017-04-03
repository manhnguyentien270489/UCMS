var module = angular.module('uccm.app.contentType.addedit');

module.controller('contentTypeAddEditCtrl', function ($scope, $xhttp, $modal, $uibModal) {
    
    $scope.type = {
        Id: 'new',
        Name: '',
        Description: '',
        IsDataList: false,
        Fields: []
    };

    $scope.submit = function() {
        var id = Utils.getParameterByName("id");
        if (id) {
            /* update */
            $xhttp.post('/api/contenttype/update', $scope.type).then(function () {
                $scope.alertSvc.addSuccess('Update Content Type successfully');
                window.setTimeout(function () {
                    location.href = '/contenttype';
                }, 1000);                
            });
        } else {
            /* create new */
            $xhttp.post('/api/contenttype/create', $scope.type).then(function () {
                $scope.alertSvc.addSuccess('Create Content Type successfully');
                window.setTimeout(function() {                    
                    location.href = '/contenttype';
                }, 1000);                
            });
        }
    }

    $scope.cancel = function() {
        location.href = '/contenttype';
    }

    $scope.createField = function() {
        var modal = $uibModal.open({
            templateUrl: 'fieldModal.html',
            controller: 'fieldModalCtrl',
            resolve: {
                field: function () { return undefined; },
                isDataList: function() { return $scope.type.IsDataList; }
            }
        });

        modal.result.then(function(field) {
            field.ContentTypeId = $scope.type.Id;
            $scope.type.Fields.push(field);
        });
    }

    $scope.edit = function(field) {
        var modal = $uibModal.open({
            templateUrl: 'fieldModal.html',
            controller: 'fieldModalCtrl',
            resolve: {
                field: function () { return field; },
                isDataList: function () { return $scope.type.IsDataList; }
            }
        });

        modal.result.then(function (newField) {
            field = _.cloneDeep(newField);
        });
    }

    $scope.delete = function(field) {
        $modal.showConfirm('Are you sure you want to delete this field?').then(function() {
            _.remove($scope.type.Fields, { Id: field.Id });
        });
    }

    $scope.init = function() {
        var id = Utils.getParameterByName("id");
        if (id) {
            $xhttp.get('/api/contenttype/getbyid?id=' + id)
                .then(function(response) {
                    $scope.type = response.data;
                });
        }
    }
});

module.controller('fieldModalCtrl', function ($scope, $xhttp, $uibModalInstance, field, isDataList) {

    $scope.field = {
        Id: 'new-' + new Date().getTime(),
        Name: '',
        DataType: 'Text',
        Length: 0,
        LookupName: '',
        ContentTypeId: '',
        Items:[]
    }
    $scope.fieldItems = '';
    $scope.datatypes = [
        'Text',
        'TextArea',
        'Checkbox',
        'Number',
        'DateTime',
        'Picklist',
        'MultiSelect',
        'Lookup'
    ];

    $scope.contentTypes = [];
    $scope.lookup = {
        Type: undefined,
        Field: undefined
    }

    $scope.namePattern = /^[a-zA-Z]{1,50}/;

    $scope.init = function() {
        if (field) {
            $scope.field = field;
            $scope.fieldItems = field.Items.join('\n');
        }

        if (isDataList) {
            $scope.datatypes = [
                'Text'
            ];
        }

        $xhttp.get('/api/contentType/GetAll').then(function (response) {
            $scope.contentTypes = _.filter(response.data, { IsDataList: true });
            if (field && field.DataType === 'Lookup') {
                $scope.lookup.Type = _.find($scope.contentTypes, { Name: field.LookupType });
                $scope.lookup.Field = _.find($scope.lookup.Type.Fields, { Name: field.LookupField });
            }
        });
    }

    $scope.init();

    $scope.ok = function () {
        if ($scope.field.DataType === 'Picklist' || $scope.field.DataType === 'MultiSelect') {
            var items = $scope.fieldItems.split('\n');
            $scope.field.Items = items;
        }
        if ($scope.field.DataType === 'Lookup') {
            $scope.field.LookupType = $scope.lookup.Type.Name;
            $scope.field.LookupField = $scope.lookup.Field.Name;
        }
        $uibModalInstance.close($scope.field);
    }

    $scope.cancel = function() {
        $uibModalInstance.dismiss('cancel');
    }
});