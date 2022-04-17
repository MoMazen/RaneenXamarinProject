const mongoose = require('mongoose');

//events using the same Category Schema
//so we reused it again

const { CategorySchema } = require('./Category.js');

const Event = mongoose.model('Event', CategorySchema);

module.exports = Event;
