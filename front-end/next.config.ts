import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  /* config options here */
    allowedDevOrigins: [
        "http://kaftp.online"
    ],
    experimental: {
      serverActions: {
        allowedOrigins: ['kaftp.online', 'localhost:3000'], 
      },
    },
};

export default nextConfig;
