
var participationspersubject = function(initialdata) {
    var self = this;

    self.isLoading = ko.observable(false);
    self.participationsPerSubject = ko.mapping.fromJS(initialdata.ParticipationsPerSubject);
    self.subjects = ko.observableArray(initialdata.Subjects);

    self.getParticipations = function(from, to, studentId) {
        _getData(from, to, studentId);
    };

    function _getData (from, to, subjectId){
         self.isLoading(true);
        
         var data = { 'from': from, 'to': to, 'id': subjectId };
        
         jQuery.ajax({
                type: 'POST',
                url: '/participation/subjectordered',
                data: data,
                traditional: true,
                success: function(result){
                    _updateModel(result);
                },
                error: function(xmlHttpRequest)
                {
                     self.isLoading(true);
                    alert(xmlHttpRequest.responseText);
                }
            });
     };
    
    function _updateModel (data) {
      self.isLoading(false);
      ko.mapping.fromJS(data.ParticipationsPerSubject, self.participationsPerSubject);
    };

    //self.dirtyItems = ko.computed(function() {
    //    return ko.utils.arrayFilter(self.participations(), function(item) {
    //        return item.dirtyFlag.isDirty();
    //    });
    //}, this);

    //self.isDirty = ko.computed(function() {
    //    return self.dirtyItems().length > 0;
    //}, this);


}