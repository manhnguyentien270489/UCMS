var module = angular.module('uccm.app.fs.folder');
module.controller('folderCtrl', function($scope, $xhttp, $modal) {
    $scope.folder = {};
    $scope.subFolders = [];
    $scope.contents = [];

    $scope.createDir = function() {
        $modal.showNewFolderDialog(null).then(function (folder) {
            folder.ParentId = $scope.folder.Id;
            $xhttp.post('/api/folder/createfolder', folder).then(function () {
                $scope.init();
            });
        });
    }

    $scope.editDir = function(dir) {
        $modal.showNewFolderDialog(dir).then(function (folder) {
            $xhttp.post('/api/folder/updatefolder', folder).then(function () {
                $scope.init();
            });
        });
    }

    $scope.deleteDir = function(dir) {
        $modal.showConfirm('Are you sure you want to delete this library and all of its contents?')
            .then(function () {
                $xhttp.delete('/api/folder/deletefolder?id=' + dir.Id).then(function () {
                    $scope.alertSvc.addSuccess('Delete library successfully');
                    $scope.init();
                    setTimeout(function () {
                        $scope.alertSvc.clear();
                    }, 1500);
                });
            });
    }

    $scope.createContent = function() {
        location.href = '/content/addedit?folderId=' + $scope.folder.Id;
    }

    $scope.editContent = function(content) {
        location.href = '/content/addedit?id=' + content.Id;
    }

    $scope.deleteContent = function(content) {
        $modal.showConfirm('Are you sure you want to delete this content?')
            .then(function () {
                $xhttp.delete('/api/content/delete?id=' + content.Id).then(function () {
                    $scope.alertSvc.addSuccess('Delete content successfully');
                    $scope.init();
                    setTimeout(function () {
                        $scope.alertSvc.clear();
                    }, 1500);
                });
            });
    }

    $scope.init = function () {
        var id = Utils.getParameterByName("id");
        $xhttp.get('/api/folder/getfolder?id=' + id).then(function(response) {
            $scope.folder = response.data;
        });

        $xhttp.get('/api/folder/getfolders?parentId=' + id).then(function (res) {
            $scope.subFolders = res.data;
        });

        $xhttp.get('/api/content/getcontents?folderId=' + id).then(function (response) {
            $scope.contents = response.data;
        });
    }
});