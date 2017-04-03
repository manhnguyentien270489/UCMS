var module = angular.module('uccm.app.contentType.list');

module.controller('contentTypeListCtrl', function ($scope, $xhttp, $modal, alertSvc) {
    $scope.alertSvc = alertSvc;
    $scope.types = [];

    $scope.createNew = function() {
        location.href = '/contentType/addedit';
    }

    $scope.edit = function(type) {
        location.href = '/contentType/addedit?id=' + type.Id;
    }

    $scope.delete = function(type) {
        $modal.showConfirm('Are you sure you want to delete this content type?')
            .then(function() {
                $xhttp.delete('/api/contenttype/delete?id=' + type.Id).then(function() {
                    $scope.init();
                });
            });
    }

    $scope.manage = function(type) {
        location.href = '/datalist/?type=' + type.Id;
    }

    $scope.init = function () {
        $xhttp.get('/api/contenttype/getall').then(function(response) {
            $scope.types = response.data;
        });
    }
});