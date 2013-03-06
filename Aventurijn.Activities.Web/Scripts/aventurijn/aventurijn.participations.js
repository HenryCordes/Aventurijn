
var participations = function(initialdata) {
    var self = this;

    self.isLoading = ko.observable(false);
    self.participations = ko.mapping.fromJS(initialdata.Participations);
    self.students = ko.observableArray(initialdata.Students);
    self.activities = ko.observableArray(initialdata.Activities);
    self.subjects = ko.observableArray(initialdata.Subjects);
    $("studentForm").validate({ submitHandler: self.save });

    self.save = function() {
        self.isLoading(true);
        var dataAsString = ko.mapping.toJSON(self.participations);
         jQuery.ajax({
                type: 'POST',
                url: 'participation/save',
                contentType: 'application/json',
                dataType: 'json',
                data: dataAsString,
                traditional: true,
                success: function(result){
                    self.isLoading(false);
                    if (result === true) {
                        puntjes.message.show("Succesvol opgeslagen");
                    }
                    else {
                         puntjes.message.show("Opslaan mislukt");
                    }
                },
                error: function(xmlHttpRequest)
                {
                    self.isLoading(false);
                    puntjes.message.show(xmlHttpRequest.responseText);
                }
            });
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
        var id = participation.ParticipationId();
        if (id == 0) {
            self.participations.remove(participation);
            puntjes.message.show("Succesvol verwijderd");
        }
        else {

            jQuery.ajax({
                type: 'POST',
                url: 'participation/remove/'+ id,
                traditional: true,
                success: function(result) {
                    if (result == id) {
                        self.participations.remove(participation);
                        puntjes.message.show("Succesvol verwijderd");
                    }
                    else {
                        puntjes.message.show("Verwijderen mislukt");
                    }
                },
                error: function(xmlHttpRequest) {
                    puntjes.message.show("Verwijderen mislukt - fout");
                    $('#exText').val(xmlHttpRequest.responseText);
                }
            });
        }
    };
    
    self.removeParticipationWithId = function(id) {                               
         self.participations.remove(function(participation) { return participation.ParticipationId == id });   
    };

    self.getParticipations = function(from, to, studentId) {
        _getData(from, to, studentId);
    };
    
    self.addActivity =  function(activity) {
        self.activities.push(activity);
        self.activities.sort(function(left, right) { return left.Name == right.Name ? 0 : (left.Name < right.Name ? -1 : 1) });
    };

    //self.dirtyItems = ko.computed(function() {
    //    return ko.utils.arrayFilter(self.participations(), function(item) {
    //        return item.dirtyFlag.isDirty();
    //    });
    //}, this);
    
    //self.isDirty = ko.computed(function() {
    //    return self.dirtyItems().length > 0;
    //}, this);
    

    function _getData (from, to, studentId){
         self.isLoading(true);
        
         var data = { 'from': from, 'to': to, 'studentId': studentId };
        
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
                     self.isLoading(true);
                    alert(xmlHttpRequest.responseText);
                }
            });
     };
    
    function _updateModel (data) {
        self.isLoading(false);
        ko.mapping.fromJS(data, self.participations);
     //   self.participations(data);
 
    };
}