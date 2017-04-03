angular.module('ngHttp',
    [
        'ngAlert',
        'ipCookie'
    ])
    .factory('$xhttp', function($http, $q, ipCookie, alertSvc) {
        var service = {
            get: function(url) {
                var defer = $q.defer();
                $http.get(url/*, {
                    headers: {
                        'X-Login-Session': ipCookie("AuthId")
                    }
                }*/).then(function(response) {
                    defer.resolve(response);
                }, function(err) {
                    alertSvc.addError(err.data.ExceptionMessage);
                    defer.reject(err);
                });
                return defer.promise;
            },

            post: function(url, data) {
                var defer = $q.defer();
                $http.post(url, data/*, {
                    headers: {
                        'X-Login-Session': ipCookie("AuthId")
                    }
                }*/).then(function(response) {
                    defer.resolve(response);
                }, function(err) {
                    alertSvc.addError(err.data.ExceptionMessage);
                    defer.reject(err);
                });
                return defer.promise;
            },
            
            delete: function(url) {
                var defer = $q.defer();
                $http.delete(url/*, {
                    headers: {
                        'X-Login-Session': ipCookie("AuthId")
                    }
                }*/).then(function(response) {
                    defer.resolve(response);
                }, function(err) {
                    alertSvc.addError(err.data.ExceptionMessage);
                    defer.reject(err);
                });

                return defer.promise;
            }
        };

        return service;
    });
