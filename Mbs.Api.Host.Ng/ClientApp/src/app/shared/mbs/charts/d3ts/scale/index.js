'use strict';

module.exports = function(d3) {
  var zoomable = require('./zoomable')(),
      util = require('../util')(),
      accessors = require('../accessor')(),
      financetime = require('./financetime')(d3.scaleLinear, d3, d3.bisect, util.rebindCallback, widen, zoomable);

  function ohlc(data, accessor) {
    accessor = accessor || accessors.ohlc();
    return d3.scaleLinear()
      .domain([d3.min(data.map(accessor.low())), d3.max(data.map(accessor.high()))].map(widen(0.02)));
  }

  function pathWithValueAccessor(data, accessor, widening) {
    accessor = accessor || accessors.value();
    widening = widening === undefined ? 0.02 : widening;
    return pathScale(d3, data, accessor, widening);
  }

  return {
    financetime: financetime,

    plot: {
      candlestick: ohlc,
      ohlc: ohlc,

      closeline: pathWithValueAccessor,
      tradeline: pathWithValueAccessor,
      valueline: pathWithValueAccessor,

      tradepoint: pathWithValueAccessor,
      valuepoint: pathWithValueAccessor,

      percent: function (scale, reference) {
        var domain = scale.domain();
        reference = reference || domain[0];
        return scale.copy().domain([domain[0], domain[domain.length-1]].map(function(d) { return (d-reference)/reference; }));
      },

      supstance: function(data, accessor) {
        accessor = accessor || accessors.supstance();
        return pathScale(d3, data, accessor.v, 0.02);
      },

      tick: ohlc,

      time: function(data, accessor) {
        accessor = accessor || accessors.value();
        return financetime().domain(data.map(accessor.t));
      },

      tradearrow: function(data, accessor) {
        accessor = accessor || accessors.trade();
        return pathScale(d3, data, accessor.p, 0.02);
      },

      trendline: function(data, accessor) {
        accessor = accessor || accessors.trendline();
        var values = mapReduceFilter(data, function(d) { return [accessor.sv(d), accessor.ev(d)]; });
        return d3.scaleLinear().domain(d3.extent(values).map(widen(0.04)));
      },

      volume: function (data, accessor) {
        accessor = accessor || accessors.ohlc().v;
        return d3.scaleLinear()
          .domain([0, d3.max(data.map(accessor))*1.15]);
      },
    }
  };
};

function pathDomain(d3, data, accessor, widening) {
  return data.length > 0 ? d3.extent(data, accessor).map(widen(widening)) : [];
}

function pathScale(d3, data, accessor, widening) {
  return d3.scaleLinear().domain(pathDomain(d3, data, accessor, widening));
}

/**
 * Only to be used on an array of 2 elements [min, max]
 * @param widening
 * @param width
 * @returns {Function}
 */
function widen(widening, width) {
  widening = widening || 0;

  return function(d, i, array) {
    if(array.length > 2) throw "array.length > 2 unsupported. array.length = " + array.length;
    width = width || (array[array.length-1] - array[0]);
    return d + (i*2-1)*width*widening;
  };
}

function mapReduceFilter(data, map) {
  return data.map(map)
    .reduce(function(a, b) { return a.concat(b); }) // Flatten
    .filter(function(d) { return d !== null; }); // Remove nulls
}
