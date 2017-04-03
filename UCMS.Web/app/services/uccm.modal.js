angular.module('uccm.modal', [
        'ui.bootstrap'
    ])
    .controller('confirmModalCtrl', function($scope, $uibModalInstance, message) {
        $scope.message = message;

        $scope.ok = function() {
            $uibModalInstance.close();
        };

        $scope.cancel = function() {
            $uibModalInstance.dismiss('cancel');
        };
    })
    .controller('folderModalCtrl', function ($scope, $uibModalInstance, folder) {
        $scope.folder = folder;

        $scope.ok = function () {
            $uibModalInstance.close($scope.folder);
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    })
    .factory('$modal', function($uibModal) {
        var service = {
            showConfirm : function(message) {
                var confirmModal = $uibModal.open({
                    templateUrl: '/app/services/template/confirmModal.html',
                    controller: 'confirmModalCtrl',
                    resolve: {
                        message: function() { return message; }
                    }
                });

                return confirmModal.result;
            },

            showNewFolderDialog: function(folder) {
                var modal = $uibModal.open({
                    templateUrl: '/app/services/template/folderModal.html',
                    controller: 'folderModalCtrl',
                    resolve: {
                        folder: function() { return folder; }
                    }
                });
                return modal.result;
            }
        }

        return service;
    });