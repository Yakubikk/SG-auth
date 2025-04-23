import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  /* config options here */
    allowedDevOrigins: [
        "http://kaftp.online"
    ],
    experimental: {
      serverActions: {
        allowedOrigins: ['kaftp.online', 'localhost:3000', 'http://kaftp.online:5000'], 
      },
    },
};

export default nextConfig;
