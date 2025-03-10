import React from "react";
import Header from "../components/Header";
import profile from "../mock-data/profile.json";

/**
 * This shows the read only view of user profile information
 * @see mock-data/profile.json for data formats
 *
 */
export default function ProfilePage() {
  return (
    <main className="px-4 py-8">
      <Header active={"profile"} />
      <div className="flex justify-center flex-col items-center">
        <div className="flex flex-col items-center self-center w-full md:w-1/2">
          <h1 className="text-gray-500 mb-4">YOUR PROFILE</h1>
          <div className="mb-8 bg-white shadow-lg rounded-lg w-full py-4 px-8 dark:bg-gray-700 dark:text-white">
            <div className="py-8 flex flex-col gap-8">
              <div className="flex text-center flex-col gap-4 lg:flex-row lg:text-left">
                <label className="flex-1" htmlFor="username">
                  Username
                </label>
                <input
                  id="username"
                  type="text"
                  value={profile.username}
                  disabled
                  className="py-2 px-4 bg-gray-200 rounded-full text-center dark:bg-gray-800"
                />
              </div>
              <div className="flex text-center flex-col gap-4 lg:flex-row lg:text-left">
                <label className="flex-1" htmlFor="password">
                  Password
                </label>
                <div className="flex flex-col gap-2">
                  <input
                    id="password"
                    type="password"
                    value="password"
                    disabled
                    className="py-2 px-4 bg-gray-200 rounded-full text-center dark:bg-gray-800"
                  />
                  <p className=" text-sm cursor-pointer hover:underline text-blue-500 lg:text-right ">
                    Reset Password
                  </p>
                </div>
              </div>
              <div className="flex text-center flex-col gap-4 lg:flex-row lg:text-left">
                <label className="flex-1" htmlFor="email">
                  Email
                </label>
                <input
                  id="email"
                  type="email"
                  value={profile.email}
                  disabled
                  className="py-2 px-4 bg-gray-200 rounded-full text-center dark:bg-gray-800"
                />
              </div>
              <div className="flex text-center flex-col gap-4 lg:flex-row lg:text-left">
                <label className="flex-1" htmlFor="phoneNumber">
                  Phone Number
                </label>
                <input
                  id="phoneNumber"
                  type="phone"
                  value={profile.phone}
                  disabled
                  className="py-2 px-4 bg-gray-200 rounded-full text-center dark:bg-gray-800"
                />
              </div>
              <div className="flex text-center flex-col gap-4 lg:flex-row lg:text-left">
                <label className="flex-1" htmlFor="alert">
                  Alert Preference
                </label>
                <div className="flex flex-col gap-2">
                  <input
                    id="alert"
                    type="text"
                    value={profile.alertPreference}
                    disabled
                    className="py-2 px-4 bg-gray-200 rounded-full text-center dark:bg-gray-800"
                  />
                  {profile.alerts.map((alert, i) => (
                    <p
                      className="text-xs text-gray-500 dark:text-gray-300"
                      key={i}
                    >
                      Alert me {alert}{" "}
                      <span className="text-blue-500 hover:underline cursor-pointer">
                        Edit
                      </span>
                    </p>
                  ))}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </main>
  );
}
