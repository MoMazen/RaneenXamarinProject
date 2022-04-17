const mongoose = require('mongoose');

const Customer = require('./Customer.js');

const AddressSchema = new mongoose.Schema({
  //id will be set by default

  address1: {
    type: mongoose.Schema.Types.String,
    required: true,
  },
  address2: {
    type: mongoose.Schema.Types.String,
    // required:true,
  },
  city: {
    type: mongoose.Schema.Types.String,
    required: true,
  },
  country: {
    type: mongoose.Schema.Types.String,
    required: true,
  },
  countryCode: {
    type: mongoose.Schema.Types.String,
    required: true,
  },
  customerId: {
    type: mongoose.Schema.Types.ObjectId,
    required: true,
  },
  province: {
    type: mongoose.Schema.Types.String,
    // required: true,
  },

  province_code: {
    type: mongoose.Schema.Types.String,
    // required: true,
  },
  zip: {
    type: mongoose.Schema.Types.String,
    required: true,
  },
});

AddressSchema.path('customerId').ref(Customer);

const Address = mongoose.model('Address', AddressSchema);

module.exports = Address;
