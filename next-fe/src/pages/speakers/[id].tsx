import { useRouter } from "next/router";
import useSWR from "swr";
import apiService from "@/shared/service/api";
import { Speaker } from "@/shared/types/Conference";
import Link from "next/link";

const SpeakerDetails = () => {
  const { query, back } = useRouter();
  const {
    data: speaker,
    error,
    isLoading,
  } = useSWR<Speaker>(`/`, () =>
    apiService.getSpeaker(query.id as unknown as number)
  );

  return (
    <>
      <ol className="breadcrumb">
        <li>
          <Link onClick={back} href="">
            Back
            <br />
          </Link>
        </li>
        <li>
          <Link href="/speakers">Speakers</Link>
        </li>
      </ol>
      {error && <p>{error.error}</p>}
      {isLoading && <p>Loading...</p>}
      {!error && !isLoading && speaker && (
        <div className="speaker-details">
          <h2>{speaker.name}</h2>
          <span className="label label-default">{speaker.bio}</span>

          <h3>Sessions</h3>
          <div className="row">
            <div className="col-md-5">
              <ul className="list-group">
                {speaker.sessions?.map((session) => (
                  <p key={session.id}>
                    <li className="list-group-item">
                      <Link
                        href="/sessions/[id]"
                        as={`/sessions/${session.id}`}
                      >
                        {session.title}
                      </Link>
                    </li>
                  </p>
                ))}
              </ul>
            </div>
          </div>
        </div>
      )}
    </>
  );
};

export default SpeakerDetails;
