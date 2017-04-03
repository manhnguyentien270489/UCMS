var module = angular.module('uccm.app.fs.library');

module.controller('libraryCtrl', function($scope, $xhttp, $uibModal, $modal) {
    $scope.libraries = [];    
    $scope.create = function() {
        $modal.showNewFolderDialog(null).then(function(folder) {
            $xhttp.post('/api/folder/createfolder', folder).then(function() {
                $scope.init();
            });
        });
    }

    $scope.edit = function(lib) {
        $modal.showNewFolderDialog(lib).then(function(folder) {
            $xhttp.post('/api/folder/updatefolder', folder).then(function() {
                $scope.init();
            });
        });
    }
    $scope.delete = function(lib) {
        $modal.showConfirm('Are you sure you want to delete this library and all of its contents?')
            .then(function() {
                $xhttp.delete('/api/folder/deletefolder?id=' + lib.Id).then(function() {
                    $scope.alertSvc.addSuccess('Delete library successfully');
                    $scope.init();
                    setTimeout(function() {
                        $scope.alertSvc.clear();
                    }, 1500);
                });
            });
    }

    $scope.init = function () {
        $xhttp.get('/api/folder/getfolders?parentId=').then(function(response) {
            $scope.libraries = response.data;
        });
    }
});