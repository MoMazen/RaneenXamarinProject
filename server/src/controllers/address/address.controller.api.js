const { request, response } = require('express');

const {
  attachAddressToClient,
} = require('../../services/address/address.service.js');

/**
 *
 * @param {request} req
 * @param {response} res
 * @param {Function} next
 */
module.exports.clientAddressAttacherControllerApi = async function (
  req,
  res,
  next
) {
  try {
    const result = await attachAddressToClient(req);
    //if all are ok
    res.status(201).json({
      success: true,
      data: result,
    });
  } catch (error) {
    res.status(500).json({
      success: false,
      error: error,
    });
  }
};
