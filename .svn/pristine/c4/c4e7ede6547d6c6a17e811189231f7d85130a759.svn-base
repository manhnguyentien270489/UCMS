var module = angular.module('uccm.app.datalist.list');

module.controller('datalistCtrl', function ($scope, $xhttp, $modal) {
    $scope.contentType = {};

    $scope.dataLists = [];

    $scope.create = function() {
        location.href = '/datalist/addedit?type=' + $scope.contentType.Id;
    }

    $scope.edit = function(list) {
        location.href = '/datalist/addedit?&type=' + $scope.contentType.Id + '&id=' + list.Id;
    }

    $scope.delete = function(list) {
        $modal.showConfirm('Are you sure you want to delete this item?').then(function() {
            $xhttp.delete('/api/datalist/delete?id=' + list.Id).then(function () {
                $scope.alertSvc.addSuccess('Delete datalist successfully.');

                setTimeout(function() {
                    $scope.alertSvc.clear();
                }, 1000);

                $scope.init();
            });
        });
    }

    $scope.init = function () {
        var id = Utils.getParameterByName("type");
        $xhttp.get('/api/contenttype/getbyid?id=' + id).then(function(response) {
            $scope.contentType = response.data;

            $xhttp.get('/api/datalist/select?typeid=' + id).then(function(response) {
                $scope.dataLists = response.data;
            });
        });
    }
});