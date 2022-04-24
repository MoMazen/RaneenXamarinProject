/**
 * @description : 'we need to serve the order that the user will make from the client'
 * @required : 'we need to provide a service to post a order and get order for specific client'
 */
const mongoDBConnectionManager = require('../../../config/db.config.js');
const { request, response } = require('express');
const Order = require('../../../models/Order');
const Product = require('../../../models/Product.js');

/**
 *
 * @param {Array<any>} products
 * @param {request} req
 */
module.exports.addOrderService = async function (req, products) {
  return new Promise(async (resolve, reject) => {
    if (mongoDBConnectionManager.connected) {
      //if the db connection is fine
      let userOrder = await Order.create({
        customer: req.user._id,
      });

      //loop on the products
      products.forEach(async (product, index) => {
        userOrder.products.push(product);
        let targetProduct = await Product.findById(product.id);
        userOrder.TotalPrice += targetProduct.price * product.amount;
        if (index == products.length - 1) {
          userOrder.markModified('TotalPrice');
          await userOrder.save();
          //if all are ok
          resolve(userOrder);
        }
      });
    } else {
      reject({
        message: 'db connection error',
      });
    }
  });
};

/**
 *
 * @param {request} req
 * @returns {promise<any>}
 */
module.exports.getOrderService = async function (req) {
  return new Promise(async (resolve, reject) => {
    if (mongoDBConnectionManager.connected) {
      //if the db connection is fine
      try {
        if (req.user) {
          const userOrder = await Order.findOne({
            customer: req.user._id,
          })
            .populate('customer')
            .populate('products.id');
          if (userOrder) {
            resolve(userOrder);
          } else {
            throw 'no orders for the user';
          }
          //if all are ok
        } else {
          reject({
            message: 'error with the authentication process',
          });
        }
      } catch (error) {
        reject(error);
      }
    } else {
      reject({
        message: 'db connection error',
      });
    }
  });
};
