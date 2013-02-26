
var participations = function (initialdata) {
    var self = this;
    
    self.loading = ko.observableArray();
    self.participations = ko.mapping.fromJS(initialdata.Participations);
    self.students = ko.observableArray(initialdata.Students);
    self.activities = ko.observableArray(initialdata.Activities);
    self.subjects = ko.observableArray(initialdata.Subjects);
   // $("studentForm").validate({ submitHandler: self.save });

    self.save = function() {
        ko.utils.postJson(location.href, { participations: ko.mapping.toJS(self.participations) });
    };
    
    self.addParticipation = function() {
        var now = new Date(); 
        self.participations.push({
            ParticipationId:  ko.observable(0),
            Participating: false,
            ParticipationDateTimeAsString: $.format.date(now,'dd-MM-yyyy'),
            ParticipationDateTime: now,
            StudentId: self.students()[0].StudentId,
            Student: self.students()[0],
            ActivityId: self.activities()[0].ActivityId,
            Activity: self.activities()[0],
            ExtraInfo:''
        });
    };
 
    self.removeParticipation = function(participation) {
        self.participations.remove(participation);
    };

    self.getParticipations = function(from, to) {
        _getData(from, to);
    };

    function _getData (from, to){
         self.loading.push(true);
        
         var data = { 'from': from, 'to': to };
        
         jQuery.ajax({
                type: 'POST',
                url: 'participation/withindaterange',
                data: data,
                traditional: true,
                success: function(result){
                    _updateModel(result);
                },
                error: function(xmlHttpRequest)
                {
                     self.loading.pop();
                    alert(xmlHttpRequest.responseText);
                }
            });
     };
    
    function _updateModel (data) {
        ko.mapping.fromJS(data, self.participations);
     //   self.participations(data);
        self.loading.pop();
    };
}