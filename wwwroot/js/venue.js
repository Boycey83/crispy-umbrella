ab.beerQuest.venue = function (venue) {
    var self = this;

    self.id = ko.observable(venue.id);
    self.name = ko.observable(venue.name);
    self.category = ko.observable(venue.category);
    self.url = ko.observable(venue.url);
    self.date = ko.observable(venue.date);
    self.excerpt = ko.observable(venue.excerpt);
    self.thumbnail = ko.observable(venue.thumbnail);
    self.lat = ko.observable(venue.lat);
    self.lng = ko.observable(venue.lng);
    self.address = ko.observable(venue.address);
    self.phone = ko.observable(venue.phone);
    self.twitter = ko.observable(venue.twitter);
    self.starsBeer = ko.observable(venue.starsBeer);
    self.starsAtmosphere = ko.observable(venue.starsAtmosphere);
    self.starsAmenities = ko.observable(venue.starsAmenities);
    self.starsValue = ko.observable(venue.starsValue);
    self.tags = ko.observableArray(venue.tags);
    
}