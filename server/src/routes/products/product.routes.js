const ApplicationRouter = require('express').Router();

const multer = require('multer');

const {
  addProductControllerApi,
  getAllProductsControllerApi,
} = require('../../controllers/product/product.controller.api.js');

ApplicationRouter.route('/api/v1/products/add').post(
  multer().fields([{ name: 'images', maxCount: 3 }]),
  addProductControllerApi
); //admin

ApplicationRouter.route('/api/v1/products').get(getAllProductsControllerApi); //client

module.exports = ApplicationRouter;
