const mongoose = require('mongoose');

const { EventEmitter } = require('events');

const DBEvents = require('./DBEvents.js');

/**
 * @description : this dbContext is using and consuming db url from the process.env
 */
class MongoDBMConnectionManager extends EventEmitter {
  constructor() {
    super();
    //states
    this.connected = false;
    this.host = '';
  }

  async init() {
    try {
      mongoose.connection.on(DBEvents.connecting, () => {
        this.emit(DBEvents.connectionState, {
          connected: false,
          status: DBEvents.connecting,
        });
      });

      mongoose.connection.on(DBEvents.connected, () => {
        this.connected = true;
        this.host = mongoose.connection.host;
        this.emit(DBEvents.connectionState, {
          connected: true,
          status: DBEvents.connected,
        });
      });

      mongoose.connection.on(DBEvents.disconnecting, () => {
        this.emit(DBEvents.connectionState, {
          connected: false,
          status: DBEvents.disconnecting,
        });
      });

      mongoose.connection.on(DBEvents.disconnected, () => {
        this.emit(DBEvents.connectionState, {
          connected: false,
          status: DBEvents.disconnected,
        });
      });

      //await for the connection
      await mongoose.connect(process.env.DB_CONNECTION_STRING);
    } catch (error) {
      this.emit(DBEvents.connectionError, error);
    }
  }
}

const mongoDBConnectionManager = new MongoDBMConnectionManager();

module.exports = mongoDBConnectionManager;
