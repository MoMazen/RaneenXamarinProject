const { request, response } = require('express');

const {
  getAllCategories,
  seedCategoriesService,
} = require('../../services/category/category.service.js');

module.exports.seedCategoriesControllerApi = async function (req, res, next) {
  try {
    await seedCategoriesService();
    //if no errors
    res.status(201).json({
      success: true,
      data: 'well done inserted all ',
    });
  } catch (error) {
    res.status(500).json({
      success: false,
      error: { message: error.message },
    });
  }
};

module.exports.getCategoriesControllerApi = async function (req, res, next) {
  try {
    const categories = await getAllCategories();

    //if all are ok

    res.status(200).json({
      success: true,
      data: categories,
    });
  } catch (error) {
    res.status(500).json({
      success: false,
      error: { message: error.message },
    });
  }
};
