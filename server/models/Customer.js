const mongoose = require('mongoose');

const CustomerSchema = new mongoose.Schema(
  {
    //id will be set automatically

    oauthId: {
      type: mongoose.Schema.Types.String,
    },

    firstName: {
      type: mongoose.Schema.Types.String,
      required: true,
    },

    lastName: {
      type: mongoose.Schema.Types.String,
      required: true,
    },

    //TODO connect last order id with the order entity
    //last order id and data -> name and etc will be included within the request if u wanna populate
    lastOrderId: {
      type: mongoose.Schema.Types.ObjectId,
      // required:true,
    },

    ordersCount: {
      type: mongoose.Schema.Types.Number,
      // required:true,
    },

    phone: {
      type: mongoose.Schema.Types.String,
      // required: true,
    },

    totalSpent: {
      type: mongoose.Schema.Types.Number,
      // required:true,
    },

    email: {
      type: mongoose.Schema.Types.String,
      required: true,
    },

    password: {
      type: mongoose.Schema.Types.String,
      // required: true,
    },

    verifiedEmail: {
      type: mongoose.Schema.Types.Boolean,
      // required:true,
    },

    company: {
      type: mongoose.Schema.Types.String,
      //   required:true
    },

    createdAt: {
      type: mongoose.Schema.Types.Date,
      default: Date.now,
    },

    updatedAt: {
      type: mongoose.Schema.Types.Date,
      default: Date.now,
    },
  },
  {
    writeConcern: {
      w: 'majority',
      j: true,
      wtimeout: 1000,
    },
  }
);

const Customer = mongoose.model('Customer', CustomerSchema);

module.exports = Customer;
